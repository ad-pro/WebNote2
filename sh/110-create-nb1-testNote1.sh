#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

NoteBookId="nb1";
NoteId="testNote1"
Title="Test Note 1"
Body="The body of Test Note 1";

${DIR}/createNote.sh "$NoteBookId" "$NoteId" "$Title" "$Body"

