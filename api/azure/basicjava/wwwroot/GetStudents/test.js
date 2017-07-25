var index = require('./index')

var context = {
    res :{},
    log : function(msg){
        //console.log(msg)
    },

    done: function(){
        console.log("done")
        console.log('---------------------')
        console.log(this.res)
        console.log('---------------------')
    }
}

var req = {}
index(context, req)
