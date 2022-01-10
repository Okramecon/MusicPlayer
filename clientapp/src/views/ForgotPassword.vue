<template>
  <div class="about">
    <h1>Reset password</h1>
  </div>
  <hr />
  <div class="row justify-content-center pb-4">
    <div class="col-md-7 col-lg-5">
      <div class="wrap">
        <div class="img"></div>
        <div class="login-wrap px-4">
          <div class="d-flex"></div>
          <form action="#" class="signin-form">
            <div class="form-group mt-3">
              <label for="username">Username</label>
              <input
                v-model="userName"
                type="text"
                class="form-control"
                required
              />
            </div>
            <hr />
            <div class="form-group mt-3">
              <label for="password">Your password</label>
              <input
                v-model="password"
                id="password-field"
                type="text"
                class="form-control"
                disabled
              />
            </div>
            <div class="form-group">
              <button type="submit" class="w-100 btn rounded submit px-3">
                Reset Password
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
import { ResetPassword } from "@/api";

export default defineComponent({
  async setup() {
    const userName = ref("");
    const password = ref("");

    const resetPassword = async () => {
      if (userName.value) {
        alert("Enter form");
        return;
      }

      password.value = await ResetPassword(userName.value);
    };

    return {
      resetPassword,
      userName,
      password,
    };
  },
});
</script>

<style src="../assets/styles/style.css"></style>
