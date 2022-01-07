<template>
  <div class="about">
    <h1>Authors</h1>
  </div>
  <hr />
  <table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Photo</th>
        <th scope="col">FullName</th>
        <th scope="col">Pseudonym</th>
        <th scope="col">Bio</th>
        <th scope="col">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="author in authors" :key="author.id">
        <th scope="row">{{ author.id }}</th>
        <th>Photo</th>
        <th>{{ author.fullName }}</th>
        <th>{{ author.pseudonym }}</th>
        <th>
          <span class="cuted-cell">{{ author.bio }}</span>
        </th>
        <td>
          <button
            type="button"
            class="btn btn-primary me-2"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            @click="prepareForAction(author), (currentAction = 'Edit')"
          >
            Edit
          </button>
          <button
            type="button"
            class="btn btn-primary ms-2"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            @click="prepareForAction(author), (currentAction = 'Delete')"
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
    Add author
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
            {{ currentAction }} Author
          </h5>
        </div>
        <div v-if="currentAction != 'Delete'" class="modal-body">
          <label for="genreInput" class="form-label">Author FullName</label>
          <input
            v-model="currentAuthorForm.fullName"
            type="text"
            class="form-control"
            id="genreInput"
            aria-describedby="genreHelp"
          />
          <label for="genreInput" class="form-label">Author Pseudonym</label>
          <input
            v-model="currentAuthorForm.pseudonym"
            type="text"
            class="form-control"
            id="genreInput"
            aria-describedby="genreHelp"
          />
          <label for="genreInput" class="form-label">Author Bio</label>
          <textarea
            v-model="currentAuthorForm.bio"
            type="text"
            class="form-control"
            id="genreInput"
            aria-describedby="genreHelp"
          />
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
            ref="closeButton"
          >
            <span> Cancel </span>
          </button>
          <button type="button" @click="actionAuthor()" class="btn btn-primary">
            <span v-if="currentAction == 'Delete'"> Yes </span>
            <span v-else>
              {{ currentAction }}
            </span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { GetAllAuthors, CreateAuthor, EditAuthor, DeleteAuthor } from "@/api";
import Author from "@/models/Author";
import { defineComponent, reactive, ref } from "vue";

export default defineComponent({
  async setup() {
    const closeButton = ref();
    const currentAuthorForm = reactive<Author>({
      id: 0,
      fullName: "",
      pseudonym: "",
      bio: "",
    });
    const currentAction = ref("Create");
    const authors = ref<Author[]>(await GetAllAuthors());

    const actionAuthor = async () => {
      if (currentAuthorForm.fullName) {
        switch (currentAction.value) {
          case "Create":
            {
              const author = await CreateAuthor(currentAuthorForm);
              if (author.id) authors.value.push(author);
            }
            closeButton.value.click();
            break;

          case "Edit": {
            const index = authors.value.findIndex(
              (g) => g.id == currentAuthorForm.id
            );
            if (~index) {
              const editedGenre = await EditAuthor(currentAuthorForm);
              authors.value.splice(index, 1, editedGenre);
            }
            closeButton.value.click();
            break;
          }
          default: {
            const index = authors.value.findIndex(
              (g) => g.id == currentAuthorForm.id
            );
            if (~index) {
              const id = await DeleteAuthor(currentAuthorForm.id);
              if (id) authors.value.splice(index, 1);
            }
            closeButton.value.click();
            break;
          }
        }
      } else alert("Enter genre name");
    };

    const prepareForAction = (author: Author) => {
      currentAuthorForm.id = author.id;
      currentAuthorForm.fullName = author.fullName;
      currentAuthorForm.pseudonym = author.pseudonym;
      currentAuthorForm.bio = author.bio;
    };

    return {
      currentAuthorForm,
      authors,
      actionAuthor,
      closeButton,
      currentAction,
      prepareForAction,
    };
  },
});
</script>
