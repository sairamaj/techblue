'use strict'
var util = require('./util')
var Promise = require('bluebird')
var ticket = Promise.promisifyAll(require('./profile'))

exports.handler = function (event, context, callback) {
    const tableName = 'studentprofile'
    console.log('table name:' + tableName)
    console.log('---------------')
    console.log(event.body)
    console.log('-------------------')
    var profile = JSON.parse(event.body)
    console.log('profile:' + JSON.stringify(profile, null, 2))
    ticket.addProfileAsync(tableName, profile)
        .then(status => {
            context.succeed(util.createResponse(200, status))
        })
        .catch(err => {
            console.log(err)
            context.fail(util.createResponse(500, err))
        })
} 