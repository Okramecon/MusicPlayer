<template>
  <div class="about">
    <h1>Users</h1>
  </div>
  <hr />
  <table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">First</th>
        <th scope="col">Last</th>
        <th scope="col">Handle</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="account in accounts" :key="account.id">
        <th scope="row">{{ account.id }}</th>
        <td>{{ account.userName }}</td>
        <td>{{ account.roleName }}</td>
        <td>@mdo</td>
      </tr>
    </tbody>
  </table>
</template>

<script lang="ts">
import { GetAllAccounts } from "@/api";
import { AccountResponse } from "@/models/response/AccountResponse";
import { defineComponent, ref } from "vue";

export default defineComponent({
  async setup() {
    const accounts = ref<AccountResponse[]>(await GetAllAccounts());

    return {
      accounts,
    };
  },
});
</script>
