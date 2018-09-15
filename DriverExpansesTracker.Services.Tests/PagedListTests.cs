using DriverExpansesTracker.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DriverExpansesTracker.Services.Tests
{
    public class PagedListTests
    {
        [Fact]
        public void PagedListCorrectCreationTest_1()
        {
            //Arrange
            var source = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            var pageNumber = 3;
            var pageSize = 3;
            var pagedList = new PagedList<int>(source.AsQueryable(), pageNumber, pageSize);

            //Act
            var actualCurrentPage = 3;
            var acutalTotalPages = 4;
            var actualPageSize = 3;
            var actualHasPrevious = true;
            var actualHasNext = true;


            //Assert
            Assert.Equal(pagedList.CurrentPage, actualCurrentPage);
            Assert.Equal(pagedList.TotalPages, acutalTotalPages);
            Assert.Equal(pagedList.PageSize, actualPageSize);
            Assert.Equal(pagedList.HasPrevious, actualHasPrevious);
            Assert.Equal(pagedList.HasNext, actualHasNext);

            Assert.DoesNotContain(1,pagedList.ToList());
            Assert.DoesNotContain(2, pagedList.ToList());
            Assert.DoesNotContain(3, pagedList.ToList());
            Assert.DoesNotContain(4, pagedList.ToList());
            Assert.DoesNotContain(5, pagedList.ToList());
            Assert.DoesNotContain(6, pagedList.ToList());
            Assert.DoesNotContain(10, pagedList.ToList());
        }

        [Fact]
        public void PagedListCorrectCreationTest_2()
        {
            //Arrange
            var source = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            var pageNumber = 1;
            var pageSize = 2;
            var pagedList = new PagedList<int>(source.AsQueryable(), pageNumber, pageSize);

            //Act
            var actualCurrentPage = 1;
            var acutalTotalPages = 5;
            var actualPageSize = 2;
            var actualHasPrevious = false;
            var actualHasNext = true;


            //Assert
            Assert.Equal(pagedList.CurrentPage, actualCurrentPage);
            Assert.Equal(pagedList.TotalPages, acutalTotalPages);
            Assert.Equal(pagedList.PageSize, actualPageSize);
            Assert.Equal(pagedList.HasPrevious, actualHasPrevious);
            Assert.Equal(pagedList.HasNext, actualHasNext);

    
            Assert.DoesNotContain(3, pagedList.ToList());
            Assert.DoesNotContain(4, pagedList.ToList());
            Assert.DoesNotContain(5, pagedList.ToList());
            Assert.DoesNotContain(6, pagedList.ToList());
            Assert.DoesNotContain(7, pagedList.ToList());
            Assert.DoesNotContain(8, pagedList.ToList());
            Assert.DoesNotContain(9, pagedList.ToList());
            Assert.DoesNotContain(10, pagedList.ToList());
        }
        [Fact]
        public void PagedListCorrectCreationTest_3()
        {
            //Arrange
            var source = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            var pageNumber = 1;
            var pageSize = 10;
            var pagedList = new PagedList<int>(source.AsQueryable(), pageNumber, pageSize);

            //Act
            var actualCurrentPage = 1;
            var acutalTotalPages = 1;
            var actualPageSize = 10;
            var actualHasPrevious = false;
            var actualHasNext = false;


            //Assert
            Assert.Equal(pagedList.CurrentPage, actualCurrentPage);
            Assert.Equal(pagedList.TotalPages, acutalTotalPages);
            Assert.Equal(pagedList.PageSize, actualPageSize);
            Assert.Equal(pagedList.HasPrevious, actualHasPrevious);
            Assert.Equal(pagedList.HasNext, actualHasNext);
        }

        [Fact]
        public void PagedListCorrectCreationTest_4()
        {
            //Arrange
            var source = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            var pageNumber = 2;
            var pageSize = 6;
            var pagedList = new PagedList<int>(source.AsQueryable(), pageNumber, pageSize);

            //Act
            var actualCurrentPage = 2;
            var acutalTotalPages = 2;
            var actualPageSize = 6;
            var actualHasPrevious = true;
            var actualHasNext = false;


            //Assert
            Assert.Equal(pagedList.CurrentPage, actualCurrentPage);
            Assert.Equal(pagedList.TotalPages, acutalTotalPages);
            Assert.Equal(pagedList.PageSize, actualPageSize);
            Assert.Equal(pagedList.HasPrevious, actualHasPrevious);
            Assert.Equal(pagedList.HasNext, actualHasNext);

            Assert.DoesNotContain(1, pagedList.ToList());
            Assert.DoesNotContain(2, pagedList.ToList());
            Assert.DoesNotContain(3, pagedList.ToList());
            Assert.DoesNotContain(4, pagedList.ToList());
            Assert.DoesNotContain(5, pagedList.ToList());
            Assert.DoesNotContain(6, pagedList.ToList());
        }

    }
}
