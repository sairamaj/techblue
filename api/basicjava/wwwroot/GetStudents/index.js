module.exports = function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');
    var GraphAPI = require('azure-graphapi');
    var Promise = require('bluebird')

    var tenant = 'fillhere'
    var clientId = 'fillhere'
    var clientSecret = 'fillhere'

    var graph = new GraphAPI(tenant, clientId, clientSecret);
    var api = Promise.promisifyAll(new GraphAPI(tenant, clientId, clientSecret))


    var students = []
    api.getAsync('groups')
        .then(groups => {
            groups.filter(g => g.displayName === 'Students').forEach(g => {
                api.getAsync('/groups/{0}/$links/members', g.objectId)
                    .then(memberLinks => {
                        var membersCount = memberLinks.length
                        var doneCount = 0
                        memberLinks.forEach(ml => {
                            var usersLinkPath = ml.url.substring('https://graph.windows.net/techblue.onmicrosoft.com/'.length)
                            console.log(usersLinkPath)
                            api.getAsync(usersLinkPath)
                                .then(user => {
                                    students.push({
                                        name: user.displayName
                                    })
                                    doneCount++
                                })
                                .catch(err => {
                                    context.res = err
                                    context.done()
                                    doneCount++
                                })
                                .finally(() => {
                                    if (doneCount === membersCount) {
                                        context.res = students
                                        context.done()
                                    }
                                })
                        })
                    })
                    .catch(err => {
                        context.res = err
                        context.done()
                    })
            })
        })
        .catch(err => {
            context.res = err
            context.done()
        })
};

