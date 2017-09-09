var azure = require('azure-storage')
module.exports = function (context, req) {
    var id = req.params.id
    var profile = JSON.parse(req.rawBody)
    let connectionString = process.env.StorageConnectionString;
    let tableService = azure.createTableService(connectionString);
    var entGen = azure.TableUtilities.entityGenerator;
    var profileInfo = {
        PartitionKey: entGen.String('allprofiles'),
        RowKey: entGen.String(id),
        data: entGen.String(req.rawBody),
    };

    context.log('inserting ' + id)
    tableService.insertEntity('profile',profileInfo, function (error, result, response) {
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