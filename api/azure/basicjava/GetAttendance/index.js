var azure = require('fast-azure-storage');


module.exports = function (context, req) {
    var date = req.params.date
    context.log(new Date().toISOString() + ' :id = >' + date)

    var options = {
        accountId: 'basicjavaclass',
        accessKey: process.env.BasicJavaClassStorageAccessKey
    };

    // Create queue, table and blob clients
    var table = new azure.Table(options);

    var op = azure.Table.Operators;
    table.queryEntities('attendance', {
        filter: azure.Table.filter([
            'PartitionKey', op.Equal, op.string(date)
        ])
    }).then(function (result) {
        console.log(result.entities)

        var users = []
        result.entities.forEach(e => {
            var userData = JSON.parse(e.data)
            var user = {
                id: e.RowKey,
                data: e.data
            }
            users.push(user)
        })
        
        context.res = users
        context.done()

    }).catch(error => {
        context.res = error
        context.done()
    })

};