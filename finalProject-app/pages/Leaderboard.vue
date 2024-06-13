<template>
	<v-container>
		<v-row>
			<v-col cols="12">
				<h1 class="display-1 text-center">Leaderboard</h1>
			</v-col>
		</v-row>
		<v-row v-for="(game, index) in games" :key="index">
			<v-col cols="12">
				<v-card class="pa-3 ma-2">
					<v-row>
						<v-col cols="3" class="subtitle-1">Player: {{ game.name }}</v-col>
						<v-col cols="3" class="subtitle-1">Score: {{ game.score }}</v-col>
						<v-col cols="3" class="subtitle-1">Lives: {{ game.lives }}</v-col>
						<v-col cols="3" class="subtitle-1">Time: {{ formatTime(game.secondsPlayed) }}</v-col>
					</v-row>
				</v-card>
			</v-col>
		</v-row>
		<v-row>
			<v-col cols="12" class="d-flex justify-center">
				<v-btn class="mx-2" color="primary" @click="router.push('/')">Home</v-btn>
				<v-btn class="mx-2" color="secondary" @click="router.push('/game')">Play Again</v-btn>
			</v-col>
		</v-row>

	</v-container>



</template>

<script setup lang="ts">
import Axios from 'axios';

const router = useRouter();

const games = ref([]);

onMounted(() => {
	Axios.get('Leaderboard/getLeaderboard')
		.then((response) => {
			console.log(response.data);
			games.value = response.data;
		})
		.catch((error) => {
			console.log(error);
		});
});

const formatTime = (seconds) => {
	const minutes = Math.floor(seconds / 60);
	const remainingSeconds = seconds % 60;
	return `${minutes}:${remainingSeconds}`;
}

</script>
