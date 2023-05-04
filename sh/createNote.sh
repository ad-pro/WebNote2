#!/bin/bash

host="https://localhost:7188"

NoteBookId=$1
NoteId=$2
Title=$3
Body=$4

data="{\"noteBook\":{\"id\":\"\",\"title\":\"\"},\
    \"note\":{\"id\":\"\",\"title\":\"${Title}\",\"body\":\"${Body}\"},\"tags\":[{\"id\":\"tag1\"},{\"id\":\"NewTagTest\"}],\"links\":[{\"noteBookId\":\"nb2-UNKNOWN\",\"noteId\":\"note-UNKNOWN\",\"id\":\"WRONG_ID\"}],\"id\":\"\"}"

echo "$data"

curl -X POST -H "Content-Type: application/json" \
    --insecure \
    -d "$data"  $host/notebooks/$NoteBookId/notes/$NoteId
