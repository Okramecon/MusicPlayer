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
            class="btn btn-primary me-2"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            @click="prepareForAction(genre), (currentAction = 'Edit')"
          >
            Edit
          </button>
          <button
            type="button"
            class="btn btn-primary ms-2"
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
    class="btn btn-primary"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
    @click="currentAction = 'Create'"
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
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            {{ currentAction }} Genre
          </h5>
        </div>
        <div v-if="currentAction != 'Delete'" class="modal-body">
          <label for="genreInput" class="form-label">Genre name</label>
          <input
            v-model="genreInputName"
            type="text"
            class="form-control"
            id="genreInput"
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
          <button
            type="button"
            @click="actionGenre()"
            class="block-button btn welcome"
          >
            <span v-if="currentAction == 'Delete'"> Yes </span>
            <span v-else> {{ currentAction }} </span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { GetAllGenres, CreateGenre, EditGenre, DeleteGenre } from "@/api";
import Genre from "@/models/Genre";
import { defineComponent, ref } from "vue";

export default defineComponent({
  async setup() {
    const closeButton = ref();
    let currentGenreId = 0;
    const genreInputName = ref("");
    const currentAction = ref("Create");
    const genres = ref<Genre[]>(await GetAllGenres());

    const actionGenre = async () => {
      if (genreInputName.value) {
        switch (currentAction.value) {
          case "Create":
            {
              const genre = await CreateGenre(genreInputName.value);
              if (genre.id) genres.value.push(genre);
            }
            closeButton.value.click();
            break;

          case "Edit": {
            const index = genres.value.findIndex((g) => g.id == currentGenreId);
            if (~index) {
              const copiedGenre: Genre = JSON.parse(
                JSON.stringify(genres.value[index])
              );
              copiedGenre.name = genreInputName.value;
              const editedGenre = await EditGenre(copiedGenre);

              genres.value.splice(index, 1, editedGenre);
            }
            closeButton.value.click();
            break;
          }
          default: {
            const index = genres.value.findIndex((g) => g.id == currentGenreId);
            if (~index) {
              const id = await DeleteGenre(currentGenreId);
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

    const prepareForAction = (currentGenre: Genre) => {
      currentGenreId = currentGenre.id;
      genreInputName.value = currentGenre.name;
    };

    return {
      genreInputName,
      genres,
      actionGenre,
      closeButton,
      prepareForAction,
      currentAction,
    };
  },
});
</script>
