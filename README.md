
> curl --request GET "http://localhost:5000/sort"
[]

> curl --request POST --header "Content-Type: application/json" --data "[2, 3, 1, 5, 3, 1, -20, 2]" "http://localhost:5000/sort"
{
  "id":"fcdffff4-1017-410c-9aa9-44c04e6aac6f",
  "status":"Pending",
  "duration":null,
  "input":[2,3,1,5,3,1,-20,2],
  "output":null
}

> curl --request GET "http://localhost:5000/sort/fcdffff4-1017-410c-9aa9-44c04e6aac6f"
{
  "id":"fcdffff4-1017-410c-9aa9-44c04e6aac6f",
  "status":"Completed",
  "duration":"00:00:05.0134826",
  "input":[2,3,1,5,3,1,-20,2],
  "output":[-20,1,1,2,2,3,3,5]
}

> curl --request GET "http://localhost:5000/sort"
[{
  "id":"fcdffff4-1017-410c-9aa9-44c04e6aac6f",
  "status":"Completed",
  "duration":"00:00:05.0134826",
  "input":[2,3,1,5,3,1,-20,2],
  "output":[-20,1,1,2,2,3,3,5]
}]
