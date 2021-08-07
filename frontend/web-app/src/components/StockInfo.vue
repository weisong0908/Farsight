<template>
  <div class="card">
    <div class="card-content">
      <div class="media">
        <div class="media-left">
          <figure class="image is-48x48">
            <img
              :src="
                stockInfo.logo ||
                  'https://bulma.io/images/placeholders/128x128.png'
              "
              alt="logo"
            />
          </figure>
        </div>
        <div class="media-content">
          <p class="title is-4">{{ stockInfo.name }}</p>
          <p class="subtitle is-6">{{ stockInfo.symbol }}</p>
        </div>
      </div>

      <div class="content">
        <p>
          {{ stockInfo.description }}
        </p>
        <p v-if="stockInfo.sector != '-'">
          {{ stockInfo.sector }}&nbsp;&#8208;&nbsp;{{ stockInfo.industry }}
        </p>
        <hr />
        <p>
          Invested Amount: <strong>{{ investedAmount }}</strong>
        </p>
        <p class="has-text-grey">Holding ID: {{ holdingId }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["stockInfo", "holdingId", "investedAmount"]
};
</script>
