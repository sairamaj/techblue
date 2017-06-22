module.exports = function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');
  var students = [
        {
            name : "sai"
        },
        {
            name : "ram"
        }
    ]
        
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: students
        };
   
    context.done();
};