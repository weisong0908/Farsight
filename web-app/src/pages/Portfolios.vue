<template>
  <page>
    <table class="table is-bordered">
      <thead>
        <tr>
          <th>Portfolio</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Portfolio 1</td>
          <td>
            <router-link to="portfolios/1">Go</router-link>
          </td>
        </tr>
        <tr>
          <td>Portfolio 2</td>
          <td>
            <router-link to="portfolios/2">Go</router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </page>
</template>

<script>
import Page from "../components/Page.vue";

export default {
  components: { Page }
};
</script>
