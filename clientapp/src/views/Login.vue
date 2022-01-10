<template>
  <div class="about">
    <h1>Login</h1>
  </div>
  <hr />
  <div class="row justify-content-center pb-4">
    <div class="col-md-7 col-lg-5">
      <div class="wrap">
        <div class="img"></div>
        <div class="login-wrap px-4">
          <div class="d-flex"></div>
          <form @submit.prevent="login" class="signin-form">
            <div class="form-group mt-3">
              <label for="username">Username</label>
              <input
                v-model="loginForm.userName"
                type="text"
                class="form-control"
                required
              />
            </div>
            <div class="form-group">
              <label for="password">Password</label>
              <input
                v-model="loginForm.password"
                id="password-field"
                type="password"
                class="form-control"
                required
              />
              <span
                toggle="#password-field"
                class="fa fa-fw fa-eye field-icon toggle-password"
              ></span>
            </div>
            <div class="form-group">
              <button type="submit" class="w-100 btn rounded submit px-3">
                Sign In
              </button>
            </div>
            <div class="d-flex justify-between">
              <div class="form-group d-md-flex">
                <div class="text-md-left">
                  <router-link to="/reset-password"
                    >Forgot Password</router-link
                  >
                </div>
              </div>
              <div class="form-group d-md-flex">
                <div class="text-md-left">
                  <router-link to="/registration">Register</router-link>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import Login from "@/models/Login";
import { LogIn } from "@/api";
import { User } from "@/models/User";
import router from "@/router";
import store from "@/store";

export default defineComponent({
  async setup() {
    const loginForm = ref<Login>(new Login());

    const login = async () => {
      if (!loginForm.value.userName && !loginForm.value.password) {
        alert("Enter form");
        return;
      }

      const user: User = await LogIn(loginForm.value);
      if (user) {
        store.commit("changeRole", user.roleName);
        localStorage.user = JSON.stringify(user);

        router.push({ name: "Home" });
      }
    };

    return {
      loginForm,
      login,
    };
  },
});
</script>

<style src="../assets/styles/style.css"></style>
