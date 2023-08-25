using GenerateLambda.Entities;
using GenerateLambda.Conditions;
using System.Data;
using GenerateLambda.Helper;
using Newtonsoft.Json;

namespace GenerateLambda;
    
class GenerateLambda
{
    static void Main(string[] args)
    {
        // create a list of events
        List<Event> events = new List<Event>
        {
            new Event { Id = 1, Name = "Event1", ProductType = "TypeA", QualityType = "High", DateTime = DateTime.Now },
            new Event { Id = 2, Name = "Event2", ProductType = "TypeB", QualityType = "Low", DateTime = DateTime.Now },
            new Event { Id = 3, Name = "Event3", ProductType = "TypeA", QualityType = "Medium", DateTime = DateTime.Now },
            // ...
        };






        // CreateGroupByKeySelector
        string groupByProperty = "QualityType"; // end-user will select this
        Func<Event, string> groupByKeySelector = GenericLambdaFunctions.CreateGroupByKeySelector<Event>(groupByProperty);

        var groupedEvents = events.GroupBy(groupByKeySelector).ToDictionary(group => group.Key, group => group.ToList());






        // CreateWhereCondition
        string filterProperty = "ProductType"; // end-user will select this
        string filterOperator = "=="; // end-user will select this
        string filterValue = "TypeA"; // end-user will select this
        Func<Event, bool> whereCondition = GenericLambdaFunctions.CreateWhereCondition<Event>(filterProperty, filterOperator, filterValue);

        var filteredEvents = events.Where(whereCondition).ToList();



        // Dynamic Lambda Functions

        DataTable filteredWorks = new DataTable();

        // sample json data 
        string jsonStr = "[{\"Id\":1,\"Name\":\"Event1\",\"ProductType\":\"TypeA\",\"QualityType\":\"High\",\"DateTime\":\"2021-09-30T15:00:00\"},{\"Id\":2,\"Name\":\"Event2\",\"ProductType\":\"TypeB\",\"QualityType\":\"Low\",\"DateTime\":\"2021-09-30T15:00:00\"},{\"Id\":3,\"Name\":\"Event3\",\"ProductType\":\"TypeA\",\"QualityType\":\"Medium\",\"DateTime\":\"2021-09-30T15:00:00\"}]";

        // convert json to datatable with json.net
        filteredWorks = (DataTable)JsonConvert.DeserializeObject(jsonStr, (typeof(DataTable)));

        // convert json to datatable with helper
        filteredWorks = DataTableHelper.GetJsonToDataTable(jsonStr);

        var dynamicList = DataTableHelper.DataTableToList(filteredWorks);

        var filteredItems = dynamicList
                       .Where(item => ((IDictionary<string, object>)item)["Name"].ToString() == "Event1")
                       .ToList();
    }
}
