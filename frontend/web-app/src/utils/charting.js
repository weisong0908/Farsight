import Chart from "chart.js/auto";

const colours = [
  "hsl(0, 0%, 29%)",
  "hsl(0, 0%, 48%)",
  "hsl(0, 0%, 71%)",
  "hsl(0, 0%, 86%)"
];

export default {
  plotPriceTrend(contextId, trend, title) {
    return new Chart(document.getElementById(contextId), {
      type: "line",
      data: {
        labels: trend.map(t => t.date),
        datasets: [
          {
            label: "Cost",
            data: trend.map(t => t.cost),
            fill: "start",
            borderColor: colours[2],
            backgroundColor: colours[2]
          },
          {
            label: "Market Price",
            data: trend.map(t => t.closePrice),
            fill: "start",
            borderColor: colours[3],
            backgroundColor: colours[3]
          }
        ]
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: title
          }
        },
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
            backgroundColor: colours,
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
  plotPie(contextId, payload, title) {
    new Chart(document.getElementById(contextId), {
      type: "doughnut",
      data: {
        labels: payload.map(p => p.label),
        datasets: [
          {
            label: "Weighting",
            data: payload.map(p => p.data),
            backgroundColor: colours,
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
  }
};
