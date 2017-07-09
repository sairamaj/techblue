var aws = require('./awsFunc')
var colors = require('colors')
var debug=require('debug')('ticket')
function addProfile(tableName, profile, callback) {
    debug('inserting' + JSON.stringify(profile))
    aws.writeDb(tableName, profile, function (err, status) {
        if (err) {
            var msg = 'err inserrting:' + JSON.stringify(profile)
            console.log(msg.red)
        }
        callback(err, status)
    })
}

function getProfile(tableName, id, callback) {
  aws.readDb(tableName, function (err, data) {
    if (err) {
      callback(err, null)
    } else {
      var profiles = []
      data.Items.forEach(function (profile) {
        profiles.push(profile)
      })
      callback(null, profiles)
    }
  })
}

module.exports.addProfile = addProfile
module.exports.getProfile = getProfile
