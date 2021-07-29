import Chart from "chart.js/auto";

export default {
  plotPriceTrend(contextId, trend) {
    new Chart(document.getElementById(contextId), {
      type: "line",
      data: {
        labels: trend.map(t => t.date),
        datasets: [
          {
            label: "Market Price",
            data: trend.map(t => t.closePrice),
            fill: "start",
            borderColor: "hsla(171, 100%, 41%, 0.5)",
            backgroundColor: "hsla(171, 100%, 41%, 0.5)"
          },
          {
            label: "Cost",
            data: trend.map(t => t.cost),
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
  },
  plotPositionPie(contextId, positions, title) {
    new Chart(document.getElementById(contextId), {
      type: "doughnut",
      data: {
        labels: positions.map(p => p.name),
        datasets: [
          {
            label: "Weighting",
            data: positions.map(p => p.value),
            backgroundColor: [
              "rgb(255, 99, 132)",
              "rgb(54, 162, 235)",
              "rgb(255, 205, 86)"
            ],
            hoverOffset: 4
          }
        ]
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: title
          },
          tooltip: {
            callbacks: {
              label: function(context) {
                const sum = context.dataset.data.reduce((pv, cv) => pv + cv, 0);
                const percentage = ((context.parsed * 100.0) / sum).toFixed(2);
                return `${context.label}: ${context.parsed}, ${percentage}%`;
              }
            }
          }
        }
      }
    });
  },
  plotPie(contextId) {
    new Chart(document.getElementById(contextId), {
      type: "doughnut",
      data: {
        labels: ["Holding 1", "Holding 2", "Holding 3"],
        datasets: [
          {
            label: "Weighting",
            data: [809, 4356, 2011],
            backgroundColor: [
              "rgb(255, 99, 132)",
              "rgb(54, 162, 235)",
              "rgb(255, 205, 86)"
            ],
            hoverOffset: 4
          }
        ]
      }
    });
  }
};
