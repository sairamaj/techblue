module.exports = function (context, req) {
    context.log('GetClassInfo.');

    var classes = []

    var doneDates = ['062717','062817','070517','070817', '071817','071917','072517',
            '072617','080117','080217','080817','080917',
            '081517','081617','082217','082317','082917','083017']
    doneDates.forEach( dt =>  {
        classes.push( {
            date: dt,
            status : 'done'
        })
    })

    /*classes.push( {
        date: '082917',
        status: 'running'
    })
 
    

     
    classes.push( {
        date: '083017',
        status: 'running'
    })
 
*/
    context.res = classes
    context.done();
};