﻿using NPerf.Lab;
using NPerfRunner.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPerfRunner.Wpf.ViewModels
{
    public class TesterViewModel : TreeViewItemViewModel, ITesterViewModel
    {
        public TesterViewModel(PerfLab lab, Type testerType)
            : base(null)
        {
            this.Name = testerType.FullName;
            var children = (ReactiveCollection<ITreeViewItemViewModel>)this.Children;
            children.AddRange(lab.TestSuites
                .Where(x => x.TesterType.Equals(testerType))
                .SelectMany(x => x.Tests)
                .Select(x => x.TestMethodName).Distinct()
                .OrderBy(x => x)
                .Select(x => new TestViewModel(this, lab, testerType, x) as ITreeViewItemViewModel));
        }     
    }
}