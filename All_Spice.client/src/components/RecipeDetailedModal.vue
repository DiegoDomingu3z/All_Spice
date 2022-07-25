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
              class="col-5 cover-img"
              :style="`background-image: url(${recipe.picture})`"
            ></div>
            <div class="col-7">
              <div class="text-end">
                <button
                  type="button"
                  class="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="row">
                <div class="col-12">
                  <div>{{ recipe.title }}</div>
                  <div class="pb-3">{{ recipe.subtitle }}</div>
                  <div class="p-2 mx-4 bg-success text-center rounded-top">
                    Recipe Steps
                  </div>
                  <div class="recipe-card mx-4 rounded-bottom">
                    <div v-for="s in step" :key="s.id" class="p-1">
                      <span>{{ s.stepPosition }}. </span>
                      <span>{{ s.body }}</span>
                    </div>
                  </div>
                  <div class="text-center mx-4">
                    <input
                      class="form-control"
                      type="text"
                      placeholder="Steps..."
                    />
                  </div>
                </div>
                <div class="col-12 pt-4">
                  <div class="p-2 mx-4 bg-success text-center rounded-top">
                    Ingredients
                  </div>
                  <div class="recipe-card mx-4 rounded-bottom">
                    <div v-for="i in ingredients" :key="i.id" class="p-1">
                      {{ i.quantity }}
                      {{ i.name }}
                    </div>
                  </div>
                  <div class="text-center mx-4">
                    <input
                      class="form-control"
                      type="text"
                      placeholder="Ingredients..."
                    />
                  </div>
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
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted, watchEffect } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { ingredientsService } from '../services/IngredientsService'
import { stepsService } from '../services/StepsService'
export default {
  setup() {
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
      async createStep() {

      }
      , recipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.activeIngredients),
      step: computed(() => AppState.activeSteps)
    }
  }
}
</script>


<style lang="scss" scoped>
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
</style>