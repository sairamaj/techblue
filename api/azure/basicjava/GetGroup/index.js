module.exports = function (context, req) {
    var groupName = req.params.name
    context.log('GetGroup started.' + groupName);
    var GraphAPI = require('azure-graphapi');
    var Promise = require('bluebird')



    var tenant = 'techblue.onmicrosoft.com'
    var clientId = '1b21fa51-4da0-45b2-859e-c638a792ef28'
    var clientSecret = 'E0DRHJn0YB6WuoPMgBFI20dRNj4GQrCBEP9yZene9yU='

    var graph = new GraphAPI(tenant, clientId, clientSecret);
    var api = Promise.promisifyAll(new GraphAPI(tenant, clientId, clientSecret))


    var students = []
    api.getAsync('groups')
        .then(groups => {
            context.log('Found groups.' + groups);
            groups.filter(g => g.displayName === groupName).forEach(g => {
                context.log('querying group users.');
                api.getAsync('/groups/{0}/$links/members', g.objectId)
                    .then(memberLinks => {
                        context.log('found members' + memberLinks.length)
                        var membersCount = memberLinks.length
                        if( membersCount == 0){
                            context.res = students
                            context.done()
                            return
                        }
                        var doneCount = 0
                        memberLinks.forEach(ml => {
                            var usersLinkPath = ml.url.substring('https://graph.windows.net/techblue.onmicrosoft.com/'.length)
                            context.log(usersLinkPath)
                            api.getAsync(usersLinkPath)
                                .then(user => {
                                    students.push({
                                        id: user.objectId,
                                        name: user.displayName,
                                        kids: user.kids,
                                        email: user.otherMails[0]
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

