<template>
  <div
    class="modal fade"
    id="recipe-modal"
    data-bs-backdrop="static"
    data-bs-keyboard="false"
    tabindex="-1"
    aria-labelledby="staticBackdropLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl rounded">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="row">
            <div
              class="col-md-5 cover-img img-fluid"
              :style="`background-image: url(${recipe.picture})`"
            >
              <button
                @click="deleteRecipe(recipe.id)"
                v-if="recipe.creatorId == account.id"
                class="btn btn-danger fab"
              >
                <i class="mdi mdi-trash-can"></i>
              </button>
            </div>
            <div class="col-md-7">
              <div class="text-end">
                <button
                  type="button"
                  class="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="row">
                <div class="col-md-12">
                  <div>{{ recipe.title }}</div>
                  <div class="pb-3">{{ recipe.subtitle }}</div>
                  <div class="p-2 mx-4 bg-success text-center rounded-top">
                    Recipe Steps
                  </div>
                  <div class="recipe-card mx-4 rounded-bottom">
                    <div v-for="s in step" :key="s.id" class="p-1 d-flex">
                      <div
                        contenteditable="true"
                        :id="`stepPosition${s.id}`"
                        @blur="editStep(s.id)"
                      >
                        {{ s.stepPosition }}
                      </div>
                      .

                      <div class="d-flex ms-2">
                        <div
                          contenteditable="true"
                          :id="`stepBody${s.id}`"
                          @blur="editStep(s.id)"
                        >
                          {{ s.body }}
                        </div>
                        <div v-if="recipe.creatorId == account.id">
                          <button @click="deleteStep(s.id)" class="btn">
                            <i class="mdi mdi-trash-can-outline"></i>
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                  <form @submit.prevent="createStep" id="step" value="reset">
                    <div
                      v-if="recipe.creatorId == account.id"
                      class="col-md-12 d-flex input-group px-4"
                    >
                      <div>
                        <input
                          class="form-control position-input"
                          type="text"
                          placeholder="step #"
                          v-model="stepData.stepPosition"
                        />
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder="Steps..."
                        v-model="stepData.body"
                      />
                      <button type="submit" class="btn btn-outline-success">
                        +
                      </button>
                    </div>
                  </form>
                </div>
                <div class="col-md-12 pt-4">
                  <div class="p-2 mx-4 bg-success text-center rounded-top">
                    Ingredients
                  </div>
                  <div class="recipe-card mx-4 rounded-bottom">
                    <div
                      v-for="i in ingredients"
                      :key="i.id"
                      class="p-1 d-flex"
                    >
                      <div>
                        <span
                          class="me-2"
                          contenteditable="true"
                          :id="`ingredientQuantity${i.id}`"
                          @blur="editIngredient(i.id)"
                        >
                          {{ i.quantity }}
                        </span>
                        <span
                          class="me-2"
                          contenteditable="true"
                          :id="`ingredientName${i.id}`"
                          @blur="editIngredient(i.id)"
                        >
                          {{ i.name }}
                        </span>
                      </div>
                      <div v-if="recipe.creatorId == account.id">
                        <button @click="deleteIngredient(i.id)" class="btn">
                          <i class="mdi mdi-trash-can-outline"></i>
                        </button>
                      </div>
                    </div>
                  </div>

                  <form
                    @submit.prevent="createIngredient"
                    id="ingredient"
                    value="reset"
                  >
                    <div
                      v-if="recipe.creatorId == account.id"
                      class="col-md-12 d-flex input-group px-4"
                    >
                      <div>
                        <input
                          class="form-control position-input"
                          type="text"
                          placeholder="quantity..."
                          v-model="ingredientData.quantity"
                        />
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder="title..."
                        v-model="ingredientData.name"
                      />
                      <button type="submit" class="btn btn-outline-success">
                        +
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted, watchEffect } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { ingredientsService } from '../services/IngredientsService'
import { stepsService } from '../services/StepsService'
import { recipesService } from '../services/RecipesService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const stepData = ref({})
    const ingredientData = ref({})
    watchEffect(async () => {
      try {
        if (AppState.activeRecipe.id) {
          await stepsService.getRecipeSteps(AppState.activeRecipe.id)
          await ingredientsService.getRecipeIngredients(AppState.activeRecipe.id)
        }
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message)
      }
    })
    return {
      stepData,
      ingredientData,
      async createStep() {
        try {
          stepData.value.recipeId = AppState.activeRecipe.id
          await stepsService.createStep(stepData.value)
          document.getElementById("step").reset();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async createIngredient() {
        try {
          ingredientData.value.RecipeId = AppState.activeRecipe.id
          await ingredientsService.createIngredient(ingredientData.value)
          document.getElementById("ingredient").reset();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async deleteStep(id) {
        try {
          if (await Pop.confirm()) {
            await stepsService.deleteStep(id)
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async deleteIngredient(id) {
        try {
          if (await Pop.confirm()) {
            await ingredientsService.deleteIngredient(id)
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async deleteRecipe(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(id)
            Modal.getOrCreateInstance(document.getElementById("recipe-modal")).hide()
          }
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async editStep(id) {
        try {
          const newPosition = document.getElementById('stepPosition' + id).innerText;
          const newBody = document.getElementById('stepBody' + id).innerText;
          await stepsService.editStep(id, newPosition, newBody)
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async editIngredient(id) {
        try {
          const newQuantity = document.getElementById("ingredientQuantity" + id).innerText
          const newName = document.getElementById("ingredientName" + id).innerText
          await ingredientsService.editIngredient(id, newQuantity, newName)
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      }
      , recipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.activeIngredients),
      step: computed(() => AppState.activeSteps),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.fab {
  height: 60px;
  width: 60px;
  border-radius: 50%;
  position: fixed;
  bottom: 3em;
  left: 9em;
  z-index: 1;
}
.cover-img {
  height: 750px;
  background-position: center;
  background-size: cover;
}

.recipe-card {
  height: 230px;
  background: #f0f4f2;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
  overflow-y: scroll;
}

.position-input {
  width: 100px;
}
</style>