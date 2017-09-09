var azure = require('fast-azure-storage');

module.exports = function (context, req) {
    var studentId = req.params.id


    var options = {
        accountId: 'basicjavaclass',
        accessKey: process.env.BasicJavaClassStorageAccessKey
    };

    // Create queue, table and blob clients
    var table = new azure.Table(options);

    var op = azure.Table.Operators;
    table.queryEntities('rewards', {
        filter: azure.Table.filter([
            'PartitionKey', op.Equal, op.string(studentId)
        ])
    }).then(function (result) {
        console.log(result.entities)

        var rewards = []
        result.entities.forEach(e => {
        var reward = {
            typeid: e.RowKey,
            status: e.status
        }
        rewards.push(reward)
    })

    context.res = rewards
    context.done()

}).catch(error => {
    context.res = error
    context.done()
})

}