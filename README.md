# textdiffcore

Crossplatform diff engine for .net core

[![Build Status](https://travis-ci.com/thezaza101/textdiffcore.svg?branch=master)](https://travis-ci.com/thezaza101/textdiffcore)


## How to use

### Install the package

Package Manager:
```cmd
PM> Install-Package textdiffcore -Version 1.0.0 
```

.NET CLI
```cmd
dotnet add package textdiffcore --version 1.0.0 
```

Packet CLI
```cmd
paket add textdiffcore --version 1.0.0 
```

### In your code...

```cs
using textdiffcore;
using textdiffcore.DiffOutputGenerators;
using textdiffcore.TextDiffEngine;


...
TextDiff diffobj = new TextDiff(new csDiff(), new HTMLDiffOutputGenerator("span", "style", "color:#003300;background-color:#ccff66;","color:#990000;background-color:#ffcc99;text-decoration:line-through;",""));

string oldText = "The quick brown fox jumps over the lazy dog";
string newText = "A quick cat jumps over the lazy sleeping dog";
string output =  diffobj.GenerateDiffOutput(oldText,newText);
```

output is:
![Output HTML](https://raw.githubusercontent.com/thezaza101/textdiffcore/master/docs/HTMLOutput.png)

Which looks like this in a web browser:
![Output HTML after rendering](https://raw.githubusercontent.com/thezaza101/textdiffcore/master/docs/HTMLOutputRender.png)

## How to implement your own output engine or text comparison engine 
TBA

## License

This software is released under the [GNU GPLv3 License](https://raw.githubusercontent.com/thezaza101/textdiffcore/master/LICENSE)

The Diff and DiffEntry class is (C) 2009 Thomas Bluemel, it is used here under its GNU GPL License.

## Making contributions
To propose a change, you first need to [create a GitHub account](https://github.com/join).

Once you're signed in, you can browse through the folders above and choose the content you're looking for. You should then see the content in Markdown form. Click the Edit icon in the top-right corner to start editing the content.

The content is written in the Markdown format. [There's a guide here on how to get started with it](https://guides.github.com/features/mastering-markdown/).

You can preview your changes using the tabs at the top of the editor.

When you're happy with your change, make sure to create a pull request for it using the options at the bottom of the page. You'll need to write a short description of the changes you've made.

A pull request is a proposal for a change to the content. Other people can comment on the change and make suggestions. When your change has been reviewed, it will be "merged" - and it will appear immediately in the published content.

Take a look at [this guide on GitHub about pull requests](https://help.github.com/articles/using-pull-requests/).


