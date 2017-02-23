using Coypu;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Zukini.UI
{
    public static class CoypuExtensions
    {
        #region Table Rows

        /// <summary>
        /// Finds all rows that are children of the current element scope. This method
        /// is usually called when the current element scope is a table.
        /// </summary>
        /// <param name="elementScope">The element scope to start search at (usually a table element).</param>
        /// <returns>Collection of rows found.</returns>
        public static IEnumerable<SnapshotElementScope> FindAllRows(this ElementScope elementScope)
        {
            return elementScope.FindAllXPath("descendant::tr");
        }

        /// <summary>
        /// Returns any child table rows that contain the specified search value.
        /// </summary>
        /// <param name="elementScope">The element scope to start search at (usually a table element).</param>
        /// <param name="searchValue">The value to search for.</param>
        /// <returns>Collection of rows found that contain the matching search value.</returns>
        public static IEnumerable<SnapshotElementScope> FindRows(this ElementScope elementScope, string searchValue)
        {
            if (String.IsNullOrEmpty(searchValue))
            {
                throw new ArgumentException("Argument searchValue must contain a value.");
            }

            IEnumerable<SnapshotElementScope> rows = elementScope.FindAllRows();
            return rows.Where(r => r.HasValue(searchValue));
        }

        /// <summary>
        /// Finds the first child row that contains the specified search value.
        /// </summary>
        /// <param name="elementScope">The element scope to start search at (usually a table element).</param>
        /// <param name="searchValue">The value to search for.</param>
        /// <returns><c>SnapshotElementScope</c> representing the first row that contains the search value. If no row is found, returns <c>null</c></returns>
        public static SnapshotElementScope FindRow(this ElementScope elementScope, string searchValue)
        {
            if (String.IsNullOrEmpty(searchValue))
            {
                throw new ArgumentException("Argument searchValue must contain a value.");
            }

            var rows = elementScope.FindAllRows();
            return rows.FirstOrDefault(r => r.HasContent(searchValue));
        }

        /// <summary>
        /// Searches a table to find rows that contain the specified search value in the specified column.
        /// </summary>
        /// <param name="elementScope">The element scope to start search at (usually a table element).</param>
        /// <param name="columnIndex">Index of the column to search.</param>
        /// <param name="searchValue">The value to search for.</param>
        /// <returns>
        /// The first row where the value in the matching column index matches the supplied search value (case sensitive). 
        /// If no row is found, returns <c>null</c>.
        /// </returns>
        public static SnapshotElementScope FindRow(this ElementScope elementScope, int columnIndex, string searchValue)
        {
            if (String.IsNullOrEmpty(searchValue))
            {
                throw new ArgumentException("Argument searchValue must contain a value.");
            }

            var rows = elementScope.FindAllRows();
            foreach(SnapshotElementScope row in rows)
            {
                var cells = row.FindAllCells();
                return cells.FirstOrDefault(cell => cell.Text == searchValue);
            }
            
            return null;
        }

        #endregion

        #region Table Cells

        /// <summary>
        /// Finds all cells that are children of the current ElementScope (usually a table row or table)
        /// </summary>
        /// <param name="elementScope">The element scope to start search at.</param>
        /// <returns>List of cells found.</returns>
        public static IEnumerable<SnapshotElementScope> FindAllCells(this ElementScope elementScope)
        {
            return elementScope.FindAllXPath("descendant::td");
        }

        /// <summary>
        /// Finds all cells that contain the provided search value.
        /// </summary>
        /// <param name="elementScope">The element scope to start search (usually a TableRow or Table).</param>
        /// <param name="searchValue">The search value to look for.</param>
        /// <returns>List of cells found that contain the provided search value.</returns>
        public static IEnumerable<SnapshotElementScope> FindCells(this ElementScope elementScope, string searchValue)
        {
            var cells = elementScope.FindAllCells();
            return cells.Where(c => c.HasContent(searchValue));
        }

        /// <summary>
        /// Finds the first cell tha matches the provided search value.
        /// </summary>
        /// <param name="elementScope">The element scope to start at (usually a TableRow or Table).</param>
        /// <param name="searchValue">The value to search for.</param>
        /// <returns>The first cell that matches the specified search value (case sensitive). If no cell is found, returns null.</returns>
        public static SnapshotElementScope FindCell(this ElementScope elementScope, string searchValue)
        {
            var cells = elementScope.FindAllCells();
            return cells.FirstOrDefault(c => c.Text == searchValue);
        }

        #endregion

        #region Native Convenience Accessors

        public static Rectangle Rectangle(this ElementScope element)
        {
            var native = (IWebElement)element.Native;
            return new Rectangle(native.Location, native.Size);
        }

        #endregion
    }
}
