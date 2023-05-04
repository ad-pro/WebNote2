#!/bin/bash

host="https://localhost:7188"

NoteBookId=$1
NoteBookTitle=$2
NoteId=$3
Title=$4
Body=$5


data="{\"noteBook\":{\"id\":\"${NoteBookId}\",\"title\":\"${NoteBookTitle}\"},\
    \"note\":{\"id\":\"${NoteId}\",\"title\":\"${Title}\",\"body\":\"${Body}\"},\"tags\":[{\"id\":\"tag1\"},{\"id\":\"NewTagTest2\"}],\"links\":[{\"noteBookId\":\"nb2-UNKNOWN\",\"noteId\":\"note-UNKNOWN\",\"id\":\"WRONG_ID\"}],\"id\":\"\"}"

echo "$data"

curl -X POST -H "Content-Type: application/json" \
    --insecure \
    -d "$data"  $host/notebooks
