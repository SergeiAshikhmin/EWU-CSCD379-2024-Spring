export class StopWatch {
	public isRunning: boolean;
	public seconds = ref(0);
	public interval: number;

	constructor() {
		this.isRunning = false;
		this.interval = 0;
	}

	public start(): void {

		if (this.isRunning) {
			return;
		}

		this.isRunning = true;
		this.interval = setInterval(() => {
			this.seconds.value++;
		}, 1000);
	}

	public stop(): void {
		clearInterval(this.interval);
		this.isRunning = false;
	}

	public reset(): void {
		this.seconds.value = 0;
	}
}
