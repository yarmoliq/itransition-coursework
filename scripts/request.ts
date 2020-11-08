function craftUrl(controller: string, action: string): string {
    return location.origin + '/' + controller + '/' + action;
}

export function sendRequest<T>(controller: string, action: string, m3thod: string, data: any): Promise<T> {
    return fetch(craftUrl(controller, action), {
        method: m3thod,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json();
        });
}