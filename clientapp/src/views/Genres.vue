<template>
  <div class="about">
    <h1>Genres</h1>
  </div>
  <hr />
  <table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Genre Name</th>
        <th scope="col" style="width: 200px">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="genre in genres" :key="genre.id">
        <th scope="row">{{ genre.id }}</th>
        <td>{{ genre.name }}</td>
        <td>
          <button
            type="button"
            class="btn me-2"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            @click="prepareForAction(genre), (currentAction = 'Edit')"
          >
            Edit
          </button>
          <button
            type="button"
            class="btn ms-2"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            @click="prepareForAction(genre), (currentAction = 'Delete')"
          >
            Delete
          </button>
        </td>
      </tr>
    </tbody>
  </table>

  <!-- Button trigger modal -->
  <button
    type="button"
    class="btn"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
    @click="prepareForAction(), (currentAction = 'Create')"
  >
    Add genre
  </button>

  <!-- Modal -->
  <div
    class="modal fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <form @submit="actionGenre()">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              {{ currentAction }} Genre
            </h5>
          </div>
          <div v-if="currentAction != 'Delete'" class="modal-body">
            <label class="form-label">Genre name</label>
            <input
              required
              v-model="currentGenreForm.name"
              type="text"
              class="form-control"
              aria-describedby="genreHelp"
            />
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="block-button btn welcome"
              data-bs-dismiss="modal"
              ref="closeButton"
            >
              <span> Cancel </span>
            </button>
            <button type="submit" class="block-button btn welcome">
              <span v-if="currentAction == 'Delete'"> Yes </span>
              <span v-else> {{ currentAction }} </span>
            </button>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { GetAllGenres, CreateGenre, EditGenre, DeleteGenre } from "@/api";
import Genre from "@/models/Genre";
import { defineComponent, reactive, ref } from "vue";

export default defineComponent({
  async setup() {
    const closeButton = ref();
    const currentGenreForm = reactive({
      id: 0,
      name: "",
    });
    const currentAction = ref("Create");
    const genres = ref<Genre[]>(await GetAllGenres());

    const actionGenre = async () => {
      if (currentGenreForm.name) {
        switch (currentAction.value) {
          case "Create":
            {
              const genre = await CreateGenre(currentGenreForm.name);
              if (genre.id) genres.value.push(genre);
            }
            closeButton.value.click();
            break;

          case "Edit": {
            const index = genres.value.findIndex(
              (g) => g.id == currentGenreForm.id
            );
            if (~index) {
              const copiedGenre: Genre = JSON.parse(
                JSON.stringify(genres.value[index])
              );
              copiedGenre.name = currentGenreForm.name;
              const editedGenre = await EditGenre(copiedGenre);

              genres.value.splice(index, 1, editedGenre);
            }
            closeButton.value.click();
            break;
          }
          default: {
            const index = genres.value.findIndex(
              (g) => g.id == currentGenreForm.id
            );
            if (~index) {
              const id = await DeleteGenre(currentGenreForm.id);
              if (id) {
                genres.value.splice(index, 1);
              }
            }
            closeButton.value.click();
            break;
          }
        }
      } else alert("Enter genre name");
    };

    const prepareForAction = (currentGenre: Genre = new Genre()) => {
      console.log(currentGenre);
      currentGenreForm.id = currentGenre.id;
      currentGenreForm.name = currentGenre.name;
    };

    return {
      currentGenreForm,
      genres,
      actionGenre,
      closeButton,
      prepareForAction,
      currentAction,
    };
  },
});
</script>
