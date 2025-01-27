# Contributing to learning-together

### Commiting
When you are commiting a change, make sure that you give the commit a title (in
imperative mood - look at examples) and a description (optional) that explains
**what** has been done and **why** it has been done. That way if something goes
wrong, we do not have to go through the commits line by line to see which one is
the one that caused a breaking change. Also please add the issue number that the
commit resolves, either in the title or description.

### Documenting the code
When writing code (especially larger chunks of code), do put a comment block
above the code which will explain, as simply, but yet in as much detail as
possible what your code does, and most importantly, sign your work with your
email. That way someone who might read your code will be able to better
understand what you did, and if they need help or they found a bug, they will
know who to talk to.

### Examples  
**CLI**  
$ git commit -m "Update README.md  
$  
$ Added chapter 'Setup'.  
$  
$ Enables team members for easier setup of the project.  
$  
$ Resolves #6"  
  
$ git commit -m "Update README.md (#6)  
$  
$ Added chapter 'About'.  
$  
$ Explains what the project is about and how it works. The chapter also contains  
$ the top 10 contributors list."
