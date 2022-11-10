interface Services {
    maxmin: string;
    sort: string;
    total: string;
    next: string;
    classify: string;
    mean: string;
}

export class ServiceURLS {
    private static instance: ServiceURLS;

    public urls: Services = {
        maxmin: "",
        sort: "",
        total: "",
        next: "",
        classify: "",
        mean: "",
    };

    private async LoadData() {
        let data = await fetch("config.json", {
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        });

        let parsed = await data.json();
        console.log("here");
        this.urls = parsed["service_urls"];
        console.log(this.urls);
    }

    constructor() {
        this.LoadData();
    }

    public static getInstance(): ServiceURLS {
        if (!this.instance) {
            this.instance = new ServiceURLS();
        }
        console.log(this.instance.urls);
        return this.instance;
    }
}
