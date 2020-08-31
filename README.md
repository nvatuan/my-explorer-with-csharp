# My C# Windows Explorer

This was one of my optional homework. Even though it was simple, it is one of my first GUI program and I have quite grown onto it. I have spent some work on the algorithms and couldn't bring myself to delete it without a proper Github repo. This repo is for the emotional value it has.

This runs on Windows only. Haven't tested it on Linux before and i doubt it could work.

## The code that dealt internally
I used extensively the Directory and Path namespace. Wrote some 1~2 depth BFS for manual browsing, Wrote a DFS for quick cd (type a valid path on the address bar),..

I actually wrote a small CLI tool that uses some of the C# Dir/Path tools before developing its GUI. You could check it out [here](https://github.com/nvatuan/codedump/blob/master/FileUtility/FileBrowser_CSharp/FileBrowser.cs)

The code in the above project is definitely the Core for this project, but not entirely.

## Some photos

Default location is `D:\` if none were mentioned (via the cmd). There were some sample files to check if it works.
<p align='center'>
<img src='https://user-images.githubusercontent.com/24392632/91754481-ac637180-ebf3-11ea-9b55-a6d9d6fa5cd1.png' alttext = '/home/'>
</p>

~~My pirated~~ A dummy directory acts as a movie directory if i have one :)

<p align='center'>
<img src='https://user-images.githubusercontent.com/24392632/91754589-e03e9700-ebf3-11ea-8489-4ad90a70fc09.png' alttext = '/tv/'>
</p>

My music directory.

<p align='center'>
<img src='https://user-images.githubusercontent.com/24392632/91754634-f51b2a80-ebf3-11ea-9529-b7d06be9108d.png' alttext = '/mu/'>
</p>

I was quite happy with the results already, so I didn't want to make any modification. I would have added something more every now and then only if i dont DISLIKE C# that much.
