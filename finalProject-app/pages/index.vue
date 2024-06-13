<template>
    <NuxtLayout>
        <v-app>
            <v-container class="d-flex justify-center align-center mt-5">
                <v-card min-width="600" min-height="300" color="secondary" class="d-flex justify-center align-center">
                    <v-row>
                        <v-col cols="12" >
                            <h1 class="display-1 text-center">Welcome to the Game!</h1>
                        </v-col>
                        <v-col cols="3" class="ml-auto mr-auto">
                            <v-btn block color="dark" @click="router.push('/game')">Start Game</v-btn>
                        </v-col>
                        <v-col cols="3" class="ml-auto mr-auto">
                            <v-btn block color="dark" @click="router.push('/leaderboard')">Leaderboard</v-btn>
                        </v-col>
                    </v-row>
                </v-card>
            </v-container>
        </v-app>
    </NuxtLayout>

</template>

<script setup lang="ts">
const router = useRouter();
</script>
