<template>
  <NavBar class="mb-2" />
  <suspense>
    <div class="container">
      <router-view />
    </div>
  </suspense>
</template>

<script lang="ts">
import { defineComponent, onMounted } from "vue";
import NavBar from "@/components/NavBar.vue";
import store from "./store";
import { User } from "./models/User";

export default defineComponent({
  components: {
    NavBar,
  },
  setup() {
    onMounted(() => {
      const userJson = localStorage.getItem("user");
      if (userJson) {
        const user = JSON.parse(userJson) as User;
        store.commit("changeRole", user.roleName);
      }
    });
  },
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
