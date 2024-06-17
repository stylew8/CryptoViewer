@section Scripts {
    <script>
        const symbol = '@ViewData["Symbol"]';
        const ws = new WebSocket(`wss://stream.binance.com:9443/ws/${symbol}@trade`);
        let stockPriceElement = document.getElementById('stock-price');
        let lastPrice = null;

        ws.onmessage = (event) => {
            let stockObject = JSON.parse(event.data);
        let price = parseFloat(stockObject.p).toFixed(2);
        stockPriceElement.innerText = price;
            stockPriceElement.style.color = !lastPrice || lastPrice == price ? 'black' : price > lastPrice ? 'green' : 'red';
        lastPrice = price;
        };

        const log = console.log;

        const chartProperties = {
            width: 900,
        height: 500,
        timeScale: {
            timeVisible: true,
        secondsVisible: false,
            }
        };

        const domElement = document.getElementById('tvchart');
        const chart = LightweightCharts.createChart(domElement, chartProperties);
        const candleSeries = chart.addCandlestickSeries();

        fetch(`https://api.binance.com/api/v3/klines?symbol=${symbol.toUpperCase()}&interval=1m&limit=1000`)
            .then(res => res.json())
            .then(data => {
                const cdata = data.map(d => {
                    return {
            time: d[0] / 1000,
        open: parseFloat(d[1]),
        high: parseFloat(d[2]),
        low: parseFloat(d[3]),
        close: parseFloat(d[4])
                    };
                });
        candleSeries.setData(cdata);
            })
            .catch(err => log(err));

        const binanceSocket = new WebSocket(`wss://stream.binance.com:9443/ws/${symbol}@kline_1m`);

        binanceSocket.onmessage = function (event) {
            const message = JSON.parse(event.data);
        const candlestick = message.k;
        const updatedData = {
            time: candlestick.t / 1000,
        open: parseFloat(candlestick.o),
        high: parseFloat(candlestick.h),
        low: parseFloat(candlestick.l),
        close: parseFloat(candlestick.c)
            };
        candleSeries.update(updatedData);
        };
    </script>
}

<div id="tvchart"></div>
<div id="stock-price"></div>
