'use strict'
var util = require('./util')
var Promise = require('bluebird')
var evt = Promise.promisifyAll(require('./profile'))

exports.handler = function (event, context, callback) {
    const tableName = 'studentprofile'
    console.log('table name:' + tableName)

    var studentId = util.getPathParameter(event, "id")
    console.log('studentId ->' + studentId)

    evt.getProfileAsync(tableName, studentId)
        .then(profiles => {
            var profile = profiles.find(p => p.id === studentId)
            context.succeed(util.createResponse(200, profile))
        })
        .catch(err => {
            console.log(err)
            context.fail(util.createResponse(500, err))
        })
} 