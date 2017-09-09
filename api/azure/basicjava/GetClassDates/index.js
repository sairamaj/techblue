module.exports = function (context, req) {
    context.log('GetClassDates.');
    context.res = ["062717","062817","070517", "070817","071817","071917"]
    context.done();
};