var azure = require('azure-storage')
module.exports = function (context, req) {
    context.log('CreateStudentReward');
    var studentId = req.params.id
    var info = JSON.parse(req.rawBody)
    var status = info.status
    var typeid = info.typeid

  let connectionString = process.env.StorageConnectionString;
    let tableService = azure.createTableService(connectionString);
    var entGen = azure.TableUtilities.entityGenerator;
    var rewardInfo = {
        PartitionKey: entGen.String(studentId),
        RowKey: entGen.String(typeid),
        status: entGen.String(status),
    };

    context.log('inserting ' + studentId + ' typeid:' + typeid + ' status:' + status)
    tableService.insertOrMergeEntity('rewards',rewardInfo, function (error, result, response) {
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
    context.done();
};