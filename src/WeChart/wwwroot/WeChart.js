
window.WeChart = {
    create: function (chartId, config, options) {
        _config = JSON.parse(config);
        _options = JSON.parse(options);
        _config.options = Object.assign({}, _config.options, _options);
        console.log( _config);
        var ctx = document.getElementById(chartId);
        let myChart = new Chart(ctx, _config);
        
        ctx['_chart'] = myChart;
    },
    update: function (chartId, labels, data, options) {
        var ctx = document.getElementById(chartId);
        let myChart = ctx['_chart'];
       // labels = JSON.parse(labels);
        let datasets=[]
        if (Array.isArray(data)) {
            data.forEach(elt => datasets.push(JSON.parse(elt)));
        } else {
            datasets.push(JSON.parse(data));
        }
       // _data = JSON.parse(data);
        _options = JSON.parse(options);
        //console.log({ labels: labels, datasets: datasets });
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