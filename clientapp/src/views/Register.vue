<template>
  <div class="about">
    <h1>Registration</h1>
  </div>
  <hr />
  <div class="row justify-content-center pb-4">
    <div class="col-md-7 col-lg-5">
      <div class="wrap">
        <div class="img"></div>
        <div class="login-wrap px-4">
          <div class="d-flex"></div>
          <form @submit.prevent="register" class="signin-form">
            <div class="form-group mt-3">
              <label for="username">Username</label>
              <input
                v-model="loginForm.userName"
                type="text"
                class="form-control"
                required
              />
            </div>
            <div class="form-group mt-3">
              <label for="username">Email</label>
              <input type="email" class="form-control" required />
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
                Register
              </button>
            </div>
            <div class="form-group d-md-flex">
              <div class="text-md-left">
                <router-link to="/login">Login</router-link>
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
import { Register } from "@/api";
import router from "@/router";

export default defineComponent({
  async setup() {
    const loginForm = ref<Login>(new Login());

    const register = async () => {
      if (!loginForm.value.userName && !loginForm.value.password) {
        alert("Enter form");
        return;
      }

      if ((await Register(loginForm.value)) == 200) {
        router.push({ name: "Login" });
      }
    };

    return {
      loginForm,
      register,
    };
  },
});
</script>

<style src="../assets/styles/style.css"></style>
