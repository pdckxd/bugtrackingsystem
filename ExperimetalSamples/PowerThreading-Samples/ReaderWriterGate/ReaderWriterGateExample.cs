/******************************************************************************
Module:  ReaderWriterGateExample.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using Wintellect.Threading;
using Wintellect.Threading.ReaderWriterGate;


///////////////////////////////////////////////////////////////////////////////


internal sealed class CatalogEntry { }
internal sealed class CatIDAndQuantity { }


///////////////////////////////////////////////////////////////////////////////


internal sealed class CatalogOrderSystem {
   private ReaderWriterGate m_gate = new ReaderWriterGate();

   public void UpdateCatalog(CatalogEntry[] catalogEntries) {
      // Perform any validation/pre-processing on catalogEntries...

      // Updating the catalog requires exclusive access to it.
      m_gate.BeginWrite(UpdateCatalog, catalogEntries, 
         delegate(IAsyncResult result) { m_gate.EndWrite(result); }, null);
   }

   // The code in this method has exclusive access to the catalog.
   private Object UpdateCatalog(ReaderWriterGateReleaser r) {
      CatalogEntry[] catalogEntries = (CatalogEntry[])r.State;
      // Update the catalog with the new entries...

      // When this method returns, exclusive access is relinquished.
      return null;
   }


   public void BuyCatalogProducts(CatIDAndQuantity[] items) {
      // Buying products requires read access to the catalog.
      m_gate.BeginRead(BuyCatalogProducts, items, delegate(IAsyncResult result) {
         m_gate.EndRead(result);
      }, null);
   }

   // The code in this method has shared read access to the catalog.
   private Object BuyCatalogProducts(ReaderWriterGateReleaser r) {
      using (r) {
         CatIDAndQuantity[] items = (CatIDAndQuantity[])r.State;
         foreach (CatIDAndQuantity item in items) {
            // Process each catalog item to build customer's order...
         }
      } // When r is Disposed, read access is relinquished.

      // Save customer's order to database
      // Send customer e-mail confirming order
      return null;
   }
}


//////////////////////////////// End of File //////////////////////////////////