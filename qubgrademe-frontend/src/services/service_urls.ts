interface Services {
    maxmin: string;
    sort: string;
    total: string;
    next: string;
    classify: string;
    mean: string;
    storage: string;
}

export class ServiceURLS {
    private static instance: ServiceURLS;

    private currProxy = 0;
    private proxy: String[] = [];
    public routes: Services = {
        maxmin: "",
        sort: "",
        total: "",
        next: "",
        classify: "",
        mean: "",
        storage: "",
    };

    private async LoadData() {
        let data = await fetch("config.json", {
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
            },
        });

        let parsed = await data.json();
        this.proxy = parsed["proxy_urls"];
        this.routes = parsed["service_routes"];
        console.log(this.routes);
        console.log(this.proxy);
    }

    private constructor() {
        this.LoadData().then(() => {
            "Config has been loaded";
        });
    }

    public static getInstance(): ServiceURLS {
        if (!this.instance) {
            this.instance = new ServiceURLS();
        }
        return this.instance;
    }

    public GetProxy(): String {
        return this.proxy[this.currProxy];
    }

    public ChangeProxy(): void {
        if (this.currProxy + 1 >= this.proxy.length) this.currProxy = 0;
        else this.currProxy++;
    }
}
