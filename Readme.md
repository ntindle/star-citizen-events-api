# Star Citizen Events API
This is a simple API to get the past events in Star Citizen. It is based on the [Star Citizen Wiki](https://robertsspaceindustries.com/comm-link/spectrum-dispatch/16259-Star-Citizen-Events-API) as well as sourced from the community.

## Usage
The API is available at [](). It is a simple GET request that returns a JSON object with the following structure:

```json
    [
        {
            "id": 1,
            "name": "Red Festival",
            "alternativeName": "Lunar New Year",
            "description": "Launch into the Lunar New Year with a bold new adventure from January 20 through February 6.\n\nTo ring in a prosperous Year of the Rabbit here on Earth in 2023, and Year of the Rooster in 2953 Stanton, we're inviting you to celebrate the Red Festival with us. As is tradition throughout the UEE, red envelopes have been hidden across Stanton, and we're offering a variety of red and gold ship paints to tempt good fortune in the year ahead.",
            "startDateTime": "2023-01-20T00:00:00",
            "endDateTime": "2023-02-06T00:00:00",
            "ingameStartDateTime": "2953-01-20T00:00:00",
            "ingameEndDateTime": "2953-02-06T00:00:00",
            "displayName": "Red Festival 2953"
        },
        {
            ...
        }
    ]
```

You can also get a single event by using the `id` parameter. For example, to get the event with the `id` of `1`, you would use the following URL: [](). This will return the following JSON object:

```json
    {
        "id": 1,
        "name": "Red Festival",
        "alternativeName": "Lunar New Year",
        "description": "Launch into the Lunar New Year with a bold new adventure from January 20 through February 6.\n\nTo ring in a prosperous Year of the Rabbit here on Earth in 2023, and Year of the Rooster in 2953 Stanton, we're inviting you to celebrate the Red Festival with us. As is tradition throughout the UEE, red envelopes have been hidden across Stanton, and we're offering a variety of red and gold ship paints to tempt good fortune in the year ahead.",
        "startDateTime": "2023-01-20T00:00:00",
        "endDateTime": "2023-02-06T00:00:00",
        "ingameStartDateTime": "2953-01-20T00:00:00",
        "ingameEndDateTime": "2953-02-06T00:00:00",
        "displayName": "Red Festival 2953"
    }
```

Notes: 
- The `id` is the unique identifier for the event. It should not be used for sorting events by time.
- The `displayName` is the name of the event with the year appended to it. This is useful for displaying the event in a list. The formula for calculating it may change as events are added.
- The `ingameStartDateTime` and `ingameEndDateTime` are the start and end dates of the event in the Star Citizen universe. This is useful for displaying the event in a list. The formula for calculating it may change as lore progresses. Currently, it just adds `930 years` to the `startDateTime` and `endDateTime` values.

## Contributing
If you would like to contribute to this project, please feel free to submit a pull request. 

### Adding an event via json file
If you would like to add an event, please add it to the `events.json` file, then make a pull request. The format is as follows:

```json
   {
    "Id": "1",
    "AlternativeName": "Lunar New Year",
    "Description": "Launch into the Lunar New Year with a bold new adventure from January 20 through February 6.\n\nTo ring in a prosperous Year of the Rabbit here on Earth in 2023, and Year of the Rooster in 2953 Stanton, we\u0027re inviting you to celebrate the Red Festival with us. As is tradition throughout the UEE, red envelopes have been hidden across Stanton, and we\u0027re offering a variety of red and gold ship paints to tempt good fortune in the year ahead.",
    "EndDateTime": "02/06/2023 00:00:00",
    "Name": "Red Festival",
    "StartDateTime": "01/20/2023 00:00:00"
  }
```

### Adding an event via api
You can also run the API locally and add an event via the `/api/events` endpoint with a POST request. Then open a pull request with the changes to the `events.json` file. The format of the `/api/events` POST endpoint is as follows:

```json
{
  "name": "string",
  "alternativeName": "string",
  "description": "string",
  "startDateTime": "2023-03-03T16:26:08.473Z",
  "endDateTime": "2023-03-03T16:26:08.473Z"
}
```

## Building
To build the project, you will need to have the [.NET Core SDK](https://dotnet.microsoft.com/download) installed. Then, you can run the following command:

```bash
dotnet build
```

## Running
To run the project, you will need to have the [.NET Core SDK](https://dotnet.microsoft.com/download) installed. 

You will also need to trust the development certificate. You can do this by running the following command:

```bash
dotnet dev-certs https --trust
```

Then, you can run the following command:

```bash
dotnet run --launch-profile https
```

## License


## Credits
