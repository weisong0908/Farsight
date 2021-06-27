import Chart from "chart.js/auto";

export default {
  plotPriceTrend(contextId, trend) {
    new Chart(document.getElementById(contextId), {
      type: "line",
      data: {
        labels: trend.map(t => t.date),
        datasets: [
          {
            label: "Value",
            data: trend.map(t => t.value * 10),
            fill: "start",
            borderColor: "hsla(171, 100%, 41%, 0.5)",
            backgroundColor: "hsla(171, 100%, 41%, 0.5)"
          },
          {
            label: "Cost",
            data: trend.map(t => t.cost * 10),
            fill: "start",
            borderColor: "hsla(171, 100%, 41%, 1)",
            backgroundColor: "hsla(171, 100%, 41%, 1)"
          }
        ]
      },
      options: {
        scales: {
          y: {
            min: 0
          }
        }
      }
    });
  }
};
