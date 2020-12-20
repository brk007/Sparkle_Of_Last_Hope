git add .

set /p input="Enter Commit Message: "
set message="%input%"

git commit -m %message%
git push -u origin master

pause