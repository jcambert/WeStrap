
window.WeChart = {
    create: function (chartId, config, options) {
        _config = JSON.parse(config);
        _options = JSON.parse(options);
        console.log(_config);

        if (_config.type =='bubble' &&  _options.tooltips.callbacks) {
            var strfn = _options.tooltips.callbacks.split(":");
            var fnName = strfn.shift();
            console.log(fnName);
            console.log(strfn.join());
            //parseFunction(strfn.join())();
           // _options.tooltips.callbacks[fnName] = parseFunction(strfn.join());
            delete _options.tooltips.callbacks;
            _options.tooltips.callbacks = {};
            _options.tooltips.callbacks[fnName] = parseFunction(strfn.join());
            console.log(_options.tooltips.callbacks);
            //_options.tooltips.callbacks.label(null, null);
        }
        _config.options = Object.assign({}, _config.options, _options);
        console.log(_config);
        var ctx = document.getElementById(chartId);
        let myChart = new Chart(ctx, _config);
        
        ctx['_chart'] = myChart;
        console.log(myChart)
    },
    update: function (chartId, labels, data, options) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
       // console.log(labels,data,options)
       // labels = JSON.parse(labels);
        let datasets=[]
        if (Array.isArray(data)) {
            data.forEach(elt => datasets.push(JSON.parse(elt)));
        } else {
            datasets.push(JSON.parse(data));
        }
       // _data = JSON.parse(data);
        _options = JSON.parse(options);
        console.log({ labels: labels, datasets: datasets,options:_options });
        myChart.data = Object.assign({}, myChart.data, {
            labels: labels,
            datasets: datasets
        });
        myChart.options = Object.assign({}, myChart.options,options);
        myChart.update();
    },
    set: function (chartId, data, options) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
        myChart.data = Object.assign(data);
        myChart.options = Object.assign(options);
        myChart.update();
    },
    reset: function (chartId) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
        myChart.reset();
    },
    clear: function (chartId) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
        myChart.clear();
    },
    stop: function (chartId) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
        myChart.stop();
    },
    destroy: function (chartId) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
        myChart.destroy();
    }


}

//https://gist.github.com/lamberta/3768814
/* Parse a string function definition and return a function object. Does not use eval.
 * @param {string} str
 * @return {function}
 *
 * Example:
 *  var f = function (x, y) { return x * y; };
 *  var g = parseFunction(f.toString());
 *  g(33, 3); //=> 99
 */
function parseFunction(str) {
    var fn_body_idx = str.indexOf('{'),
        fn_body = str.substring(fn_body_idx + 1, str.lastIndexOf('}')),
        fn_declare = str.substring(0, fn_body_idx),
        fn_params = fn_declare.substring(fn_declare.indexOf('(') + 1, fn_declare.lastIndexOf(')')),
        args = fn_params.split(',');

    args.push(fn_body);

    function Fn() {
        return Function.apply(this, args);
    }
    Fn.prototype = Function.prototype;

    return new Fn();
}