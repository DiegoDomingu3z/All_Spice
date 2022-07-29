<template>
  <form @submit.prevent="createRecipe" id="recipe">
    <div class="row">
      <div class="col-6">
        <label for="">Title</label>
        <input
          type="text"
          class="form-control"
          placeholder="Title..."
          v-model="recipeData.title"
        />
      </div>
      <div class="col-6">
        <label for="">Category</label>
        <input
          type="text"
          class="form-control"
          placeholder="Category..."
          v-model="recipeData.category"
        />
      </div>
      <div class="col-12 pt-3">
        <label for="">Picture</label>
        <input
          type="text"
          class="form-control"
          placeholder="Picture Url..."
          v-model="recipeData.picture"
        />
      </div>
      <div class="col-12 pt-3">
        <label for="">Sub Title</label>
        <textarea
          maxlength="50"
          placeholder="Details"
          class="form-control"
          name=""
          id=""
          cols="53"
          rows="1"
          v-model="recipeData.subTitle"
        ></textarea>
        <p class="text-muted">A brief description of the recipe</p>
      </div>
      <div class="col-12 text-end">
        <button type="button" class="btn btn-white" data-bs-dismiss="modal">
          Cancel
        </button>
        <button class="btn btn-success" type="submit">submit</button>
      </div>
    </div>
  </form>
</template>


<script>
import { ref } from '@vue/reactivity'
import { recipesService } from '../services/RecipesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const recipeData = ref({})
    return {
      recipeData,
      async createRecipe() {
        try {
          await recipesService.createRecipe(recipeData.value)

          Modal.getOrCreateInstance(document.getElementById('recipe')).hide();
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>