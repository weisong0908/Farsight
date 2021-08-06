<template>
  <div>
    <h1 class="subtitle">Summary</h1>
    <nav class="level">
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Total Return</p>
          <p
            :class="
              totalReturn >= 0
                ? 'title has-text-success'
                : 'title has-text-danger'
            "
          >
            <span>{{ totalReturn }}</span>
            <span
              >&nbsp;({{ ((totalReturn / invested) * 100).toFixed(2) }} %)
            </span>
          </p>
        </div>
      </div>
    </nav>
    <nav class="level">
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Invested</p>
          <p class="title">{{ invested }}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Capital Gain</p>
          <p class="title">{{ capitalGain }}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Dividend</p>
          <p class="title">{{ dividend }}</p>
        </div>
      </div>
    </nav>
    <canvas id="breakdown"></canvas>
  </div>
</template>

<script>
import dashboardWidgetsService from "../services/dashboardWidgetService";
import pageMixin from "../mixins/page";
import charting from "../utils/charting";

export default {
  data() {
    return {
      invested: 0,
      capitalGain: 0,
      dividend: 0
    };
  },
  computed: {
    totalReturn() {
      return this.capitalGain + this.dividend;
    }
  },
  mixins: [pageMixin],
  async created() {
    const accessToken = this.getAccessToken();
    const { data } = await dashboardWidgetsService.getSummaryWidgetData(
      accessToken
    );
    this.invested = data.invested;
    this.capitalGain = data.capitalGain;
    this.dividend = data.dividend;

    charting.plotPie(
      "breakdown",
      [
        {
          label: "Invested",
          data: this.invested
        },
        {
          label: "Capital Gain",
          data: this.capitalGain
        },
        {
          label: "Dividend",
          data: this.dividend
        }
      ],
      "Breakdown"
    );
  }
};
</script>
