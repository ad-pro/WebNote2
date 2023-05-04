#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

NoteBookId="testNb1";
NoteBookTitle="Test NoteBook 1"
NoteId="testNote2"
Title="Test Note 2"
Body="The body of Test Note 2";

${DIR}/createNoteBookAndNote.sh "$NoteBookId" "$NoteBookTitle" "$NoteId" "$Title" "$Body"

