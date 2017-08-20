var azure = require('azure-storage')
module.exports = function (context, req) {
    context.log('Creating attendance.');
    var id = req.params.id
    var info = JSON.parse(req.rawBody)
    var date = info.date

  let connectionString = process.env.StorageConnectionString;
    let tableService = azure.createTableService(connectionString);
    var entGen = azure.TableUtilities.entityGenerator;
    var profileInfo = {
        PartitionKey: entGen.String(date),
        RowKey: entGen.String(id),
        data: entGen.String(req.rawBody),
    };

    context.log('inserting ' + id)
    tableService.insertEntity('attendance',profileInfo, function (error, result, response) {
        if(error){
            context.log('error:' + error)
            context.res = {
                   status: 400,
                   result : "error",
                   error :  error
            }
        }else{
            context.log('success:')
            context.res = {
                result : "success"
            }
        }

        context.done()
    })

};