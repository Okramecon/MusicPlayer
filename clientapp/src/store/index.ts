import { createStore } from "vuex";

export default createStore({
  state: {
    authorized: false,
    role: "",
  },
  mutations: {
    changeAuthorize(state, result: boolean) {
      state.authorized = result;
    },
    changeRole(state, role: string) {
      state.role = role;
    },
  },
  actions: {},
  modules: {},
});
