using CsharpAlgorithmsAndDataStructures.DataStructures;
using FluentAssertions;
using Xunit;

namespace CsharpAlgorithmsAndDataStructures.Tests
{
    public class TrieTests
    {
        [Fact]
        public void AddWord_CommonRootWords_CorrectState()
        {
            var sut = new Trie();
            
            sut.AddWord("cat").AddWord("car");

            sut.SuggestNextCharacters("c").Should().Equal("a");
            sut.SuggestNextCharacters("ca").Should().Equal("tr");
            sut.SuggestNextCharacters("cat").Should().BeEmpty();
            sut.SuggestNextCharacters("x").Should().BeNull();
            sut.DoesWordExist("cat").Should().BeTrue();
            sut.DoesWordExist("ca").Should().BeFalse();
        }

        [Fact]
        public void AddWord_LongerAfterShorter_KeepsShort()
        {
            var sut = new Trie();
            
            sut.AddWord("cat");
            sut.AddWord("category");

            sut.DoesWordExist("cat").Should().BeTrue();
            sut.DoesWordExist("category").Should().BeTrue();
            sut.DoesWordExist("ca").Should().BeFalse();
            sut.DoesWordExist("cate").Should().BeFalse();
        }
    }
}