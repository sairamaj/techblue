var azure = require('azure-storage')

module.exports = function (context, req) {
    var id = req.params.id
    context.log(new Date().toISOString() + ' :id = >' + id)

    let connectionString = process.env.StorageConnectionString;
    let tableService = azure.createTableService(connectionString);

    context.log(new Date().toISOString() + ' :retriving...')
    tableService.retrieveEntity('profile','allprofiles',id, function (error, result, response){
        if( error ){
            context.res = {
                result: 'error',
                error: error
            }
        }else{
            context.log(new Date().toISOString() + ':response' + JSON.stringify(response.body.data))
            context.res = response.body.data
        }
       
       context.done()
    })
};